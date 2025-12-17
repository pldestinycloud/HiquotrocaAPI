using Hiquotroca.API.Domain.Common;
using Hiquotroca.API.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;

namespace Hiquotroca.API.Domain.Entities.Lottery
{
    public class Lottery : BaseEntity
    {
        protected Lottery() { }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public float TicketPrice { get; private set; }
        public int TotalTickets { get; private set; }
        public int TicketsSold { get; private set; }
        public int MinTicketsSold { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public string? ImageUrl { get; private set; }
        public bool IsActive { get; private set; }
        public int WinnerNumber { get; private set; } = 0;
        public List<Ticket> Tickets { get; set; } = new List<Ticket>(); 

        public Lottery(string title, string description, float ticketPrice, int totalTickets, int minTicketsSold, DateTime expiryDate, string imageUrl)
        {
            Title = title;
            Description = description;
            TicketPrice = ticketPrice;
            TotalTickets = totalTickets;
            MinTicketsSold = minTicketsSold;
            ExpiryDate = expiryDate;
            ImageUrl = imageUrl;
            TicketsSold = 0;
            IsActive = true;
        }


        public Lottery Update(string title, string description, float ticketPrice, DateTime expiryDate, string? imageUrl, bool isActive)
        {
            Title = title;
            Description = description;
            TicketPrice = ticketPrice;
            ExpiryDate = expiryDate;
            ImageUrl = imageUrl;
            IsActive = isActive;

            return this;
        }

        public void RegisterTicketSale(Ticket ticket)
        {
            Tickets.Add(ticket);
            TicketsSold++;
        }

        public void PurchaseTicket(User user, int selectedNumber)
        {
            if (!IsActive || ExpiryDate < DateTime.UtcNow)
                throw new InvalidOperationException("This lottery is not active or has expired.");

            if (TicketsSold >= TotalTickets)
                throw new InvalidOperationException("All tickets have been sold.");

            if (selectedNumber < 1 || selectedNumber > TotalTickets)
                throw new InvalidOperationException($"Invalid number. Must be between 1 and {TotalTickets}.");

            bool numberTaken = Tickets.Any(t => t.SelectedNumber == selectedNumber);
            if (numberTaken)
                throw new InvalidOperationException($"Number {selectedNumber} has already been purchased.");


            var ticket = new Ticket((int)this.Id, user.Id, selectedNumber);
            RegisterTicketSale(ticket);
        }

        public double GetTicketPrice()
        {
            return TicketPrice;
        }

        public Lottery DeactivateLottery()
        {
            if (!IsActive)
                throw new InvalidOperationException("Lottery is already closed.");
            IsActive = false;

            return this;
        }

        public Lottery SetWinner()
        {
            if(IsActive)
                throw new InvalidOperationException("Lottery is still active. Cannot declare a winner.");

            //Maybe deactivate in according to business rules in the future
            if (TicketsSold <  Math.Floor((double)TotalTickets/2))
                throw new InvalidOperationException("Minimum tickets sold not reached. Cannot declare a winner.");

            if (!Tickets.Any())
                throw new InvalidOperationException("No tickets have been sold.");

            var random = new Random();
            int winningIndex = random.Next(Tickets.Count);
            var winningTicket = Tickets[winningIndex];

            WinnerNumber = winningTicket.SelectedNumber;

            return this;
        }

        public Ticket? GetWinnerTicket()
        {
            if (WinnerNumber == 0)
                throw new InvalidOperationException("Winner has not been declared yet.");
            return Tickets.FirstOrDefault(t => t.SelectedNumber == WinnerNumber);
        }
    }
}