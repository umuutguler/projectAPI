using Entities.DataTransferObjects;
using Entities.Models;
using Iyzipay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IPaymentService
    {
        Payment MakePayment(User user, PaymentDto paymentDto, ReservationInfo reservationInfo);
    }
}
