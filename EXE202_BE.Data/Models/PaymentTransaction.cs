using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public class PaymentTransaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public long OrderCode { get; set; } // Mã đơn hàng (tương ứng với orderCode gửi lên PayOS)

    public int Amount { get; set; } // Số tiền

    public string Description { get; set; } // Mô tả giao dịch

    public string? PaymentLinkId { get; set; } // ID liên kết thanh toán (từ PayOS)

    public string Status { get; set; } // Trạng thái: PENDING, SUCCESS, FAILED

    [Required]
    public int UPId { get; set; } // ID của UserProfile thực hiện giao dịch

    [ForeignKey("UPId")]
    public virtual UserProfiles UserProfile { get; set; } // Liên kết với UserProfiles

    public DateTime CreatedAt { get; set; } // Thời gian tạo giao dịch

    public DateTime? UpdatedAt { get; set; } // Thời gian cập nhật trạng thái
}