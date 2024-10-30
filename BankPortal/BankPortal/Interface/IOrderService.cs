using BankPortal.Entities;
using BankPortal.Models.Dto;
using System.Transactions;

namespace BankPortal.Interface
{
    public interface IOrderService
    {
        Task ProcessOrder(OrderDto order);
    }
}
