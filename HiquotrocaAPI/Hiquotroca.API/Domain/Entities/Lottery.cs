using Hiquotroca.API.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace Hiquotroca.API.Domain.Entities
{
    public class Lottery : BaseEntity
    {
        protected Lottery() { }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal TicketPrice { get; private set; }
        public int TotalTickets { get; private set; }
        public int TicketsSold { get; private set; }
        public int MinTicketsSold { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public string? ImageUrl { get; private set; }
        public bool IsActive { get; private set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>(); 

        public Lottery(string title, string description, decimal ticketPrice, int totalTickets, int minTicketsSold, DateTime expiryDate, string imageUrl)
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


        public Lottery Update(string title, string description, decimal ticketPrice, DateTime expiryDate, string? imageUrl, bool isActive)
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
    }
}