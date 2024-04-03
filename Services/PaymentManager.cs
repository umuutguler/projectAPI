using Entities.DataTransferObjects;
using Entities.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Services.Contracts;

namespace Services
{
    public class PaymentManager : IPaymentService
    {
        private User? _user;
        public Payment MakePayment(User user, PaymentDto paymentDto, ReservationInfo reservationInfo)
        {
            // İyzico ödeme seçeneklerinin tanımlanması
            Options options = new Options();
            options.ApiKey = "sandbox-CxFMJiRR0kISQiRxLZA67DcFQjWN4JBC";
            options.SecretKey = "sandbox-3x51sfH47ANSOq9zB4uOohQwjIxVk3rT";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            // Ödeme isteğinin oluşturulması
            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = reservationInfo.ReservationPrice.ToString();
            double totalPriceAsInt = int.Parse(request.Price) + (int.Parse(request.Price) * 0.05);
            request.PaidPrice = totalPriceAsInt.ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            // Ödeme kartı bilgilerinin tanımlanması
            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = paymentDto.CardName;
            paymentCard.CardNumber = paymentDto.CardNumber.Replace(" ", "");
            paymentCard.ExpireMonth = paymentDto.ExpirationMonth;
            paymentCard.ExpireYear = paymentDto.ExpirationYear;
            paymentCard.Cvc = paymentDto.Cvv;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();


            buyer.Id = user.Id;
            buyer.Name = user.FirstName;
            buyer.Surname = user.LastName;
            buyer.GsmNumber = user.PhoneNumber;
            buyer.Email = user.Email;
            // Bundan sonrası değiştirilmedi
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = reservationInfo.Id.ToString();
            firstBasketItem.Name = $"Reservation with id {reservationInfo.Id} and fee {reservationInfo.ReservationPrice}";
            firstBasketItem.Category1 = "Reservation";
            firstBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            firstBasketItem.Price = request.Price;
            basketItems.Add(firstBasketItem);

            request.BasketItems = basketItems;

            // Ödemenin gerçekleştirilmesi ve sonucun dönüşü
            return Payment.Create(request, options);

        }
    }
}