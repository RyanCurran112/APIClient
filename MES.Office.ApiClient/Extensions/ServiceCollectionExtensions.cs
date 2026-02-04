using MES.Office.ApiClient.Configuration;
using MES.Office.ApiClient.Handlers;
using MES.Office.ApiClient.Objects.Common;
using MES.Office.ApiClient.Objects.Logistics;
using MES.Office.ApiClient.Objects.Organization;
using MES.Office.ApiClient.Objects.Production;
using MES.Office.ApiClient.Objects.Purchasing;
using MES.Office.ApiClient.Objects.Sales;
using MES.Office.ApiClient.Objects.Stock;
using MES.Office.ApiClient.Objects.UserManagement;
using MES.Office.ApiClient.Services;
using MES.Office.ApiClient.Transactions.Purchasing;
using MES.Office.ApiClient.Transactions.Sales;
using MES.Office.ApiClient.Transactions.Stock;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Configuration;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Common;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Logistics;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Organization;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Production;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Purchasing;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Sales;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.Stock;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Objects.UserManagement;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Purchasing;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Sales;
using MES.Office.WebAPI.Contracts.Interfaces.v1.ApiClients.Transactions.Stock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace MES.Office.ApiClient.Extensions
{
    /// <summary>
    /// Extension methods for registering API clients with dependency injection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all API client services with the specified base URL
        /// </summary>
        public static IServiceCollection AddMesApiClients(this IServiceCollection services, string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("Base URL cannot be null or empty", nameof(baseUrl));

            // Stock Transaction API Clients
            services.AddHttpClient<IStockMovement_ApiClient, StockMovement_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<StockMovement_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new StockMovement_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IStockIssue_ApiClient, StockIssue_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<StockIssue_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new StockIssue_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IStockAdjustment_ApiClient, StockAdjustment_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<StockAdjustment_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new StockAdjustment_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IStockBinTransfer_ApiClient, StockBinTransfer_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<StockBinTransfer_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new StockBinTransfer_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IInterWarehouseTransfer_ApiClient, InterWarehouseTransfer_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<InterWarehouseTransfer_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new InterWarehouseTransfer_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IStockReservation_ApiClient, StockReservation_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<StockReservation_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new StockReservation_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IStockQuarantine_ApiClient, StockQuarantine_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<StockQuarantine_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new StockQuarantine_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IStockIssueRequest_ApiClient, StockIssueRequest_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<StockIssueRequest_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new StockIssueRequest_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IStockBinTransferRequest_ApiClient, StockBinTransferRequest_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<StockBinTransferRequest_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new StockBinTransferRequest_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IStockTake_ApiClient, StockTake_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<StockTake_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new StockTake_ApiClient(httpClient, logger, config);
            });

            // Sales Transaction API Clients
            services.AddHttpClient<ISalesOrder_ApiClient, SalesOrder_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<SalesOrder_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new SalesOrder_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<ISalesDeliveryNote_ApiClient, SalesDeliveryNote_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<SalesDeliveryNote_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new SalesDeliveryNote_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<ISalesPickingNote_ApiClient, SalesPickingNote_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<SalesPickingNote_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new SalesPickingNote_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<ISalesReturn_ApiClient, SalesReturn_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<SalesReturn_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new SalesReturn_ApiClient(httpClient, logger, config);
            });

            // Purchasing Transaction API Clients
            services.AddHttpClient<IPurchaseOrder_ApiClient, PurchaseOrder_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<PurchaseOrder_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new PurchaseOrder_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IPurchaseDeliveryNote_ApiClient, PurchaseDeliveryNote_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<PurchaseDeliveryNote_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new PurchaseDeliveryNote_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IPurchaseReturn_ApiClient, PurchaseReturn_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<PurchaseReturn_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new PurchaseReturn_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IShippingNote_ApiClient, ShippingNote_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<ShippingNote_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new ShippingNote_ApiClient(httpClient, logger, config);
            });

            // Stock Object API Clients
            services.AddHttpClient<ILocation_ApiClient, Location_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Location_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Location_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IProduct_ApiClient, Product_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Product_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Product_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IProductBatch_ApiClient, ProductBatch_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<ProductBatch_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new ProductBatch_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IProductCategory_ApiClient, ProductCategory_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<ProductCategory_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new ProductCategory_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<ISite_ApiClient, Site_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Site_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Site_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IStockBin_ApiClient, StockBin_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<StockBin_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new StockBin_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IStockMovementReason_ApiClient, StockMovementReason_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<StockMovementReason_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new StockMovementReason_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IUnitOfMeasure_ApiClient, UnitOfMeasure_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<UnitOfMeasure_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new UnitOfMeasure_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IWarehouse_ApiClient, Warehouse_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Warehouse_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Warehouse_ApiClient(httpClient, logger, config);
            });

            // Sales Object API Clients
            services.AddHttpClient<ICustomer_ApiClient, Customer_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Customer_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Customer_ApiClient(httpClient, logger, config);
            });

            // Purchasing Object API Clients
            services.AddHttpClient<ISupplier_ApiClient, Supplier_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Supplier_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Supplier_ApiClient(httpClient, logger, config);
            });

            // Logistics Object API Clients
            services.AddHttpClient<ICarrier_ApiClient, Carrier_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Carrier_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Carrier_ApiClient(httpClient, logger, config);
            });

            // Organization Object API Clients
            services.AddHttpClient<IDepartment_ApiClient, Department_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Department_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Department_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IProject_ApiClient, Project_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Project_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Project_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<ICompany_ApiClient, Company_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Company_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Company_ApiClient(httpClient, logger, config);
            });

            // User Management API Clients
            services.AddHttpClient<IPermission_ApiClient, Permission_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Permission_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Permission_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IRole_ApiClient, Role_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Role_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Role_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IUser_ApiClient, User_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<User_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new User_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<ISecurityGroup_ApiClient, SecurityGroup_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<SecurityGroup_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new SecurityGroup_ApiClient(httpClient, logger, config);
            });

            // Production Object API Clients
            services.AddHttpClient<IMachine_ApiClient, Machine_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Machine_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Machine_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IWorkOrder_ApiClient, WorkOrder_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<WorkOrder_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new WorkOrder_ApiClient(httpClient, logger, config);
            });

            // Common Object API Clients
            services.AddHttpClient<IPartner_ApiClient, Partner_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Partner_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Partner_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IAuditLogItem_ApiClient, AuditLogItem_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<AuditLogItem_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new AuditLogItem_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IAddress_ApiClient, Address_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Address_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Address_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IContact_ApiClient, Contact_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Contact_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Contact_ApiClient(httpClient, logger, config);
            });

            // Configuration API Clients
            services.AddHttpClient<IBusinessRule_ApiClient, BusinessRule_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<BusinessRule_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new BusinessRule_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IEntityTransactionNumber_ApiClient, EntityTransactionNumber_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<EntityTransactionNumber_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new EntityTransactionNumber_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<ILanguage_ApiClient, Language_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Language_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Language_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IInternalLanguage_ApiClient, InternalLanguage_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<InternalLanguage_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new InternalLanguage_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<ILocalizedMessage_ApiClient, LocalizedMessage_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<LocalizedMessage_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new LocalizedMessage_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<ILocalization_ApiClient, Localization_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<Localization_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new Localization_ApiClient(httpClient, logger, config);
            });

            services.AddHttpClient<IWorkflowStatus_ApiClient, WorkflowStatus_ApiClient>((sp, client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
            })
            .AddTypedClient((httpClient, sp) =>
            {
                var logger = sp.GetService<ILoggerFactory>()?.CreateLogger<WorkflowStatus_ApiClient>();
                var config = sp.GetService<IConfiguration>();
                return new WorkflowStatus_ApiClient(httpClient, logger, config);
            });

            return services;
        }

        /// <summary>
        /// Registers all API client services using base URL from configuration
        /// </summary>
        public static IServiceCollection AddMesApiClients(this IServiceCollection services,
            Action<ApiClientOptions> configureOptions)
        {
            var options = new ApiClientOptions();
            configureOptions(options);

            return services.AddMesApiClients(options.BaseUrl);
        }

        /// <summary>
        /// Registers all API client services with authentication support
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="baseUrl">Base URL for the API</param>
        /// <param name="enableAuthentication">Whether to enable automatic JWT authentication</param>
        /// <param name="defaultCompanyId">Optional default company ID for multi-tenancy</param>
        public static IServiceCollection AddMesApiClientsWithAuth(
            this IServiceCollection services,
            string baseUrl,
            bool enableAuthentication = true,
            long? defaultCompanyId = null)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("Base URL cannot be null or empty", nameof(baseUrl));

            if (enableAuthentication)
            {
                // Register authentication service
                services.AddHttpClient<IApiAuthenticationService, ApiAuthenticationService>((sp, client) =>
                {
                    client.BaseAddress = new Uri(baseUrl);
                });

                // Register authentication handler
                services.AddTransient<AuthenticationHandler>(sp =>
                {
                    var authService = sp.GetRequiredService<IApiAuthenticationService>();
                    var logger = sp.GetService<ILogger<AuthenticationHandler>>();
                    return new AuthenticationHandler(authService, logger, defaultCompanyId);
                });
            }

            // Register all API clients (without auth - they'll use the handler if enabled)
            return services.AddMesApiClients(baseUrl);
        }

        /// <summary>
        /// Registers all API client services using configuration with optional authentication
        /// </summary>
        public static IServiceCollection AddMesApiClientsWithAuth(
            this IServiceCollection services,
            Action<ApiClientOptionsWithAuth> configureOptions)
        {
            var options = new ApiClientOptionsWithAuth();
            configureOptions(options);

            return services.AddMesApiClientsWithAuth(
                options.BaseUrl,
                options.EnableAuthentication,
                options.DefaultCompanyId);
        }
    }

    /// <summary>
    /// Configuration options for API clients
    /// </summary>
    public class ApiClientOptions
    {
        public string BaseUrl { get; set; } = string.Empty;
    }

    /// <summary>
    /// Configuration options for API clients with authentication
    /// </summary>
    public class ApiClientOptionsWithAuth : ApiClientOptions
    {
        /// <summary>
        /// Whether to enable automatic JWT authentication (default: true)
        /// </summary>
        public bool EnableAuthentication { get; set; } = true;

        /// <summary>
        /// Default company ID for multi-tenancy (optional)
        /// </summary>
        public long? DefaultCompanyId { get; set; }
    }
}
