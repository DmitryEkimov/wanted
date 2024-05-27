namespace Wanted.Bus;

public sealed class RabbitMqConfig
{
    public string Host { get; set; } = default!;
    public string VirtualHost { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}
