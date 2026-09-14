namespace apitest.DTO.DapperDTO;

public class OrdersDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string OrderDate { get; set; }
    public string Status { get; set; }
    public decimal TotalPrice { get; set; }
}