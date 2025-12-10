using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hiquotroca.API.Domain.Common;

namespace Hiquotroca.API.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public Ticket(int lotteryId, long userId, int selectedNumber)
        {
            LotteryId = lotteryId;
            UserId = userId;
            SelectedNumber = selectedNumber;
            PurchaseDate = DateTime.UtcNow;
        }

        protected Ticket() { }

        public int SelectedNumber { get; private set; } // O número escolhido (Ex: 111)
        public DateTime PurchaseDate { get; private set; }

        public long UserId { get; private set; }

        public int LotteryId { get; private set; }

        [ForeignKey("LotteryId")]
        public Lottery? Lottery { get; private set; }
    }
}