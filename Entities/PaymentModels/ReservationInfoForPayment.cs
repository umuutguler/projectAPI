using Iyzipay.Model;

namespace Entities.PaymentModels
{
    public class ReservationInfoForPayment : BasketItem
    {
        public string ReservationId { get; set; }
        public decimal Price { get; set; }

        public string ToPKIRequestString()
        {
            return base.ToPKIRequestString();
        }
    }
}
