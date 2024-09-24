using AutoMapper;
using Newtonsoft.Json;
using NanoNet.MessageBus;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using NanoNet.Services.ShoppingCartAPI.Data;
using NanoNet.Services.ShoppingCartAPI.Dtos;
using NanoNet.Services.ShoppingCartAPI.Models;
using NanoNet.Services.ShoppingCartAPI.SettingData;
using NanoNet.Services.ShoppingCartAPI.ErrorHandling;
using NanoNet.Services.ShoppingCartAPI.Interfaces.IService;

namespace NanoNet.Services.ShoppingCartAPI.Services;
public class CouponService(IHttpClientFactory httpClientFactory, IMapper mapper, IProductService productService, CartDbContext cartDbContext,
ICouponService couponService, IOptions<TopicAndQueueNames> topicAndQueueNames, IMessageBusService messageBusService) : ICouponService
{
    private readonly TopicAndQueueNames _topicAndQueueNames = topicAndQueueNames.Value;

    public async Task<CouponDto> GetCouponByCode(string couponCode)
    {
        using var client = httpClientFactory.CreateClient("Coupon");

        var response = await client.GetAsync($"/api/coupon/GetCouponByCode/{couponCode}");

        var content = await response.Content.ReadAsStringAsync();

        var data = JsonConvert.DeserializeObject<ResponseDto>(content);

        if (data is null || !data.IsSuccess) return new CouponDto();

        var coupon = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(data.Result) ?? string.Empty);

        return coupon ?? new CouponDto();
    }

    public async Task<Result<CartDto>> GetCart(string userId)
    {
        var cartHeader = mapper.Map<CartHeaderDto>(cartDbContext.CartHeaders.First(x => x.UserId == userId));

        if(cartHeader is null)
            return Result.Failure<CartDto>("Cart Not Found");

        var cartItems = mapper.Map<IEnumerable<CartItemDto>>(cartDbContext.CartItems.Where(x => x.CartHeaderId == cartHeader.Id));

        if(cartItems is null)
            return Result.Failure<CartDto>("Cart Items Not Found");

        var productList = await productService.GetProducts();

        var cartDto = new CartDto
        {
            CartHeader = cartHeader,
            CartItems = cartItems
        };

        foreach (var item in cartDto.CartItems)
        {
            item.Product = productList.FirstOrDefault(x => x.Id == item.ProductId);

            cartDto.CartHeader.TotalPrice += item.Count * item.Product!.Price;
        }

        if (string.IsNullOrEmpty(cartDto.CartHeader.CouponCode)) 
            return Result.Success(cartDto);

        var coupon = await couponService.GetCouponByCode(cartDto.CartHeader.CouponCode);

        if (!(cartDto.CartHeader.TotalPrice >= coupon.MinAmount)) 
            return Result.Success(cartDto);

        cartDto.CartHeader.TotalPrice -= coupon.DiscountAmount;
        cartDto.CartHeader.Discount = coupon.DiscountAmount;

        return Result.Success(cartDto);
    }

    public async Task<Result<CartDto>> CartUpsert(CartDto cartDto)
    {
        var cartHeader = await cartDbContext.CartHeaders.AsNoTracking()
            .FirstOrDefaultAsync(x => x.UserId == cartDto.CartHeader.UserId);

        if (cartHeader == null)
        {
            // create cart header
            var cartHeaderForAdd = mapper.Map<CartHeaderDto, CartHeader>(cartDto.CartHeader);

            cartDbContext.CartHeaders.Add(cartHeaderForAdd);

            await cartDbContext.SaveChangesAsync();

            // create cart Item
            cartDto.CartItems.First().CartHeaderId = cartHeaderForAdd.Id;

            var cartItemForAdd = mapper.Map<CartItemDto, CartItem>(cartDto.CartItems.First());

            cartDbContext.CartItems.Add(cartItemForAdd);
        }
        else
        {
            // we sure now that header is not null so we can check if Items exist
            var cartItems = await cartDbContext.CartItems.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductId == cartDto.CartItems.First().ProductId && x.CartHeaderId == cartHeader.Id);

            if (cartItems == null)
            {
                cartDto.CartItems.First().CartHeaderId = cartHeader.Id;

                var cartItemForAdd = mapper.Map<CartItemDto, CartItem>(cartDto.CartItems.First());

                cartDbContext.CartItems.Add(cartItemForAdd);
            }
            else
            {
                // update cart Item
                cartDto.CartItems.First().Count += cartItems.Count;

                cartDto.CartItems.First().CartHeaderId = cartItems.CartHeaderId;

                cartDto.CartItems.First().Id = cartItems.Id;

                cartDbContext.CartItems.Update(mapper.Map<CartItemDto, CartItem>(cartDto.CartItems.First()));
            }
        }

        await cartDbContext.SaveChangesAsync();

        return Result.Success(cartDto);
    }

    public async Task<Result<bool>> RemoveCartItem(int cartItemId)
    {
        var cartItem = await cartDbContext.CartItems.FirstOrDefaultAsync(x => x.Id == cartItemId);

        if (cartItem is null) return Result.Failure<bool>("Cart item not found");

        var cartItemsCount = await cartDbContext.CartItems.Where(x => x.CartHeaderId == cartItem.CartHeaderId).CountAsync();

        cartDbContext.CartItems.Remove(cartItem);

        if (cartItemsCount == 1)
        {
            var cartHeaderToRemove = await cartDbContext.CartHeaders.FirstOrDefaultAsync(x => x.Id == cartItem.CartHeaderId);

            if (cartHeaderToRemove is not null)
                cartDbContext.CartHeaders.Remove(cartHeaderToRemove);
        }

        await cartDbContext.SaveChangesAsync();

        return Result.Success(true);
    }

    public async Task<Result<bool>> ApplyOrRemoveCoupon(CartDto cartDto)
    {
        var cartHeaderFromDb = await cartDbContext.CartHeaders.FirstAsync(x => x.UserId == cartDto.CartHeader.UserId);

        cartHeaderFromDb.CouponCode = cartDto.CartHeader.CouponCode;

        cartDbContext.CartHeaders.Update(cartHeaderFromDb);

        await cartDbContext.SaveChangesAsync();

        return Result.Success(true);
    }

    public async Task<Result<bool>> EmailCartRequest(CartDto cartDto)
    {
        await messageBusService.PublishMessage(_topicAndQueueNames.EmailShoppingCartQueue, cartDto);

        return Result.Success(true);
    }

}