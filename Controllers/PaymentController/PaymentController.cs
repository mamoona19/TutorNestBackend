using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TutorNest.API.DTOs.Payment;
using TutorNest.API.Interfaces;
using TutorNest.API.Services.Parent;

namespace TutorNest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IParentService _parentService;

        public PaymentController(
            IPaymentService paymentService,
            IParentService parentService)
        {
            _paymentService = paymentService;
            _parentService = parentService;
        }

        // ==========================================
        // ADMIN - VERIFY PAYMENT
        // ==========================================

        [HttpPost("admin/verify/{paymentId:long}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> VerifyPayment(long paymentId)
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out var verifiedBy))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid user."
                });
            }

            var result =
                await _paymentService.VerifyPaymentAsync(
                    paymentId,
                    verifiedBy
                );

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Payment not found."
                });
            }

            return Ok(new
            {
                success = true,
                message = result.Message,
                data = result
            });
        }


        // ==========================================
        // PARENT - GET MY INVOICES
        // ==========================================

        [HttpGet("parent/invoices")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> GetParentInvoices()
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid user."
                });
            }

            var parentId =
                await _parentService.GetParentIdByUserIdAsync(userId);

            if (!parentId.HasValue)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Parent profile not found."
                });
            }

            var invoices =
                await _paymentService.GetParentInvoicesAsync(
                    parentId.Value
                );

            return Ok(new
            {
                success = true,
                data = invoices
            });
        }
        [HttpPost("parent/pay")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> PayInvoice(
      [FromBody] CreatePaymentDto request)
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid user."
                });
            }

            var parentId =
                await _parentService.GetParentIdByUserIdAsync(userId);

            if (!parentId.HasValue)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Parent profile not found."
                });
            }

            if (request.InvoiceId <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid invoice."
                });
            }

            var result =
                await _paymentService.CreatePaymentAsync(
                    request.InvoiceId,
                    parentId.Value,
                    request.PaymentMethodId,
                    request.PaymentGatewayId,
                    request.TransactionReference
                );

            if (result == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Unable to create payment."
                });
            }

            return Ok(new
            {
                success = true,
                message = result.Message,
                data = result
            });
        }
        // ==========================================
        // ADMIN - GET PAYMENT ACCOUNTS
        // ==========================================

        [HttpGet("admin/payment-accounts")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetPaymentAccounts()
        {
            var accounts =
                await _paymentService.GetPaymentAccountsAsync();

            return Ok(new
            {
                success = true,
                data = accounts
            });
        }
        // ==========================================
        // ADMIN - CREATE PAYMENT ACCOUNT
        // ==========================================

        [HttpPost("admin/payment-accounts")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> SavePaymentAccount(
            [FromBody] SavePaymentAccountDto request)
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out var adminId))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid admin user."
                });
            }

            if (request.PaymentMethodId <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Payment method is required."
                });
            }

            if (string.IsNullOrWhiteSpace(request.AccountName))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Account name is required."
                });
            }

            var result =
                await _paymentService.SavePaymentAccountAsync(
                    request,
                    adminId
                );

            return Ok(new
            {
                success = true,
                message = "Payment account saved successfully.",
                data = result
            });
        }
        // ==========================================
        // ADMIN - UPDATE PAYMENT ACCOUNT
        // ==========================================

        [HttpPut("admin/payment-accounts/{paymentAccountId:long}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdatePaymentAccount(
            long paymentAccountId,
            [FromBody] SavePaymentAccountDto request)
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out var adminId))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid admin user."
                });
            }

            if (paymentAccountId <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Invalid payment account."
                });
            }

            var result =
                await _paymentService.UpdatePaymentAccountAsync(
                    paymentAccountId,
                    request,
                    adminId
                );

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Payment account not found."
                });
            }

            return Ok(new
            {
                success = true,
                message = "Payment account updated successfully.",
                data = result
            });
        }
        // ==========================================
        // ADMIN - PAYMENT ACCOUNT STATUS
        // ==========================================

        [HttpPatch("admin/payment-accounts/{paymentAccountId:long}/status")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdatePaymentAccountStatus(
            long paymentAccountId,
            [FromBody] UpdatePaymentAccountStatusDto request)
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out var adminId))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid admin user."
                });
            }

            var result =
                await _paymentService.UpdatePaymentAccountStatusAsync(
                    paymentAccountId,
                    request.IsActive,
                    adminId
                );

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Payment account not found."
                });
            }

            return Ok(new
            {
                success = true,
                message = request.IsActive
                    ? "Payment account activated successfully."
                    : "Payment account deactivated successfully.",
                data = result
            });
        }
    }
}