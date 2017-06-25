using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Wilson.Accounting.Core.Entities.ValueObjects
{
    [JsonObject]
    public class ListOfPayments : ValueObject<ListOfPayments>, IEnumerable<Payment>
    {
        [JsonProperty]
        private IList<Payment> Payments { get; set; }

        protected ListOfPayments()
        {
        }

        public static ListOfPayments Create()
        {
            return new ListOfPayments() { Payments = new List<Payment>() };
        }

        public static ListOfPayments Create(IEnumerable<Payment> payments)
        {
            return new ListOfPayments() { Payments = payments.ToList() };
        }

        public decimal Sum()
        {
            return this.Payments.Sum(x => x.Amount);
        }

        public ListOfPayments Add(Payment payment)
        {
            this.Payments.Add(payment);
            return ListOfPayments.Create(this.Payments);
        }

        public IEnumerator<Payment> GetEnumerator()
        {
            return this.Payments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected override bool EqualsCore(ListOfPayments other)
        {
            return Payments.OrderBy(x => x.Date).ThenBy(x => x.Amount)
                .SequenceEqual(other.Payments.OrderBy(x => x.Date).ThenBy(x => x.Amount));
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = this.Payments.GetHashCode();
                return hashCode;
            }
        }

        public static explicit operator ListOfPayments(string paymetnsList)
        {
            var payments = JsonConvert.DeserializeObject<IEnumerable<Payment>>(paymetnsList);

            return Create(payments);
        }

        public static implicit operator string(ListOfPayments listOfPayments)
        {
            return JsonConvert.SerializeObject(listOfPayments.Payments);
        }
    }
}
