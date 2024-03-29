﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Infrastructure;
using WebApplication.Model;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly List<Order> Orders = new();
        
        [HttpPost]
        public ActionResult<IEnumerable<Order>> Post(Order order)
        {
            var orderValidator = new OrderValidator(order);

            if(!orderValidator.IsValidOrder())
                return BadRequest();
            
            order.Book.Genre = default;
            order.Person.DOB = default;
            order.TimeOfReceipt = DateTimeOffset.UtcNow;
            Orders.Add(order);

            return new ObjectResult(Orders);
        }
    }
}