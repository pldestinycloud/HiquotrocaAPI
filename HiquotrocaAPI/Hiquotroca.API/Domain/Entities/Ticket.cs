using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;

namespace Hiquotroca.API.Domain.Entities
{
    public class Ticket 
    {
        public int SelectedNumber { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public long UserId { get; private set; }
        public long LotteryId { get; private set; }

        public Ticket(int lotteryId, long userId, int selectedNumber)
        {
            LotteryId = lotteryId;
            UserId = userId;
            SelectedNumber = selectedNumber;
            PurchaseDate = DateTime.UtcNow;
        }
        protected Ticket() { }
    }
}