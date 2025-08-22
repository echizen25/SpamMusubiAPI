using Dapper;
using System.Data;
using SpamMusubiAPI.DTOs;
using SpamMusubiAPI.Repositories.Interfaces;

namespace SpamMusubiAPI.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly IDbConnection _db;
    public OrdersRepository(IDbConnection db) => _db = db;

    public async Task<IEnumerable<OrderDto>> GetAllAsync(DateTime? from = null, DateTime? to = null, string? status = null)
    {
        var sql = @"SELECT order_id AS OrderId, batch AS Batch, name_customer AS NameCustomer, menu_order AS MenuOrder, add_ons AS AddOns, quantity AS Quantity, care_of AS CareOf, payment_scheme AS PaymentScheme, status AS Status, date_delivered AS DateDelivered, date_paid AS DatePaid, created_at AS CreatedAt
                    FROM orders
                    WHERE (@from IS NULL OR date_delivered >= @from)
                      AND (@to IS NULL OR date_delivered < DATEADD(day, 1, @to))
                      AND (@status IS NULL OR status = @status)
                    ORDER BY date_delivered DESC, created_at DESC;";
        return await _db.QueryAsync<OrderDto>(sql, new { from, to, status });
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        var sql = @"SELECT order_id AS OrderId, batch AS Batch, name_customer AS NameCustomer, menu_order AS MenuOrder, add_ons AS AddOns, quantity AS Quantity, care_of AS CareOf, payment_scheme AS PaymentScheme, status AS Status, date_delivered AS DateDelivered, date_paid AS DatePaid, created_at AS CreatedAt
                    FROM orders WHERE order_id=@Id";
        return await _db.QueryFirstOrDefaultAsync<OrderDto>(sql, new { Id = id });
    }

    public async Task<IEnumerable<OrderDto>> GetByDayAsync(DateTime date)
    {
        var sql = @"SELECT order_id AS OrderId, batch AS Batch, name_customer AS NameCustomer, menu_order AS MenuOrder, add_ons AS AddOns, quantity AS Quantity, care_of AS CareOf, payment_scheme AS PaymentScheme, status AS Status, date_delivered AS DateDelivered, date_paid AS DatePaid, created_at AS CreatedAt
                    FROM orders
                    WHERE CAST(date_delivered AS date) = CAST(@date AS date)
                    ORDER BY created_at DESC;";
        return await _db.QueryAsync<OrderDto>(sql, new { date });
    }

    public async Task<IEnumerable<(DateTime Day, int Count, decimal Total)>> GetCalendarSummaryAsync(DateTime from, DateTime to)
    {
        var sql = @"SELECT CAST(o.date_delivered AS date) AS Day, COUNT(*) AS Count, CAST(SUM(o.quantity * (m.price + COALESCE(a.price,0))) AS DECIMAL(12,2)) AS Total
                    FROM orders o
                    JOIN main_menu m ON o.menu_order = m.menu_id
                    LEFT JOIN adds_on a ON o.add_ons = a.adds_on_id
                    WHERE o.date_delivered >= @from AND o.date_delivered < DATEADD(day, 1, @to)
                    GROUP BY CAST(o.date_delivered AS date)
                    ORDER BY Day;";
        var rows = await _db.QueryAsync(sql, new { from, to });
        return rows.Select(r => (Day: (DateTime)r.Day, Count: (int)r.Count, Total: (decimal)r.Total));
    }

    public async Task<int> CreateAsync(OrderDto dto)
    {
        var sql = @"INSERT INTO orders (batch, name_customer, menu_order, add_ons, quantity, care_of, payment_scheme, status, date_delivered, date_paid)
                    VALUES (@Batch, @NameCustomer, @MenuOrder, @AddOns, @Quantity, @CareOf, @PaymentScheme, @Status, @DateDelivered, @DatePaid);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
        return await _db.ExecuteScalarAsync<int>(sql, dto);
    }

    public async Task<int> UpdateAsync(OrderDto dto)
    {
        var sql = @"UPDATE orders SET
                        batch=@Batch,
                        name_customer=@NameCustomer,
                        menu_order=@MenuOrder,
                        add_ons=@AddOns,
                        quantity=@Quantity,
                        care_of=@CareOf,
                        payment_scheme=@PaymentScheme,
                        status=@Status,
                        date_delivered=@DateDelivered,
                        date_paid=@DatePaid
                    WHERE order_id=@OrderId";
        return await _db.ExecuteAsync(sql, dto);
    }

    public async Task<int> DeleteAsync(int id)
    {
        var sql = "DELETE FROM orders WHERE order_id=@Id";
        return await _db.ExecuteAsync(sql, new { Id = id });
    }

    public async Task<int> UpdateStatusAsync(int id, string status)
    {
        var sql = "UPDATE orders SET status=@Status WHERE order_id=@Id";
        return await _db.ExecuteAsync(sql, new { Status = status, Id = id });
    }
}
