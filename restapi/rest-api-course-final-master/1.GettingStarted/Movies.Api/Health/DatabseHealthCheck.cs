using Microsoft.Extensions.Diagnostics.HealthChecks;
using Movies.Application.Database;

namespace Movies.Api.Health
{
    public class DatabseHealthCheck : IHealthCheck
    {
        public const string Name = "Database";
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<DatabseHealthCheck> _logger;

        public DatabseHealthCheck(
            IDbConnectionFactory dbConnectionFactory, 
            ILogger<DatabseHealthCheck> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                _ = await _dbConnectionFactory.CreateConnectionAsync(cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                const string errorMessage = "Database is unhealthy";
                _logger.LogError(errorMessage, ex);
                return HealthCheckResult.Unhealthy(errorMessage, ex);
            }
        }
    }
}
