using Grpc.Core;
using GrpcService;

namespace GrpcService.Services;

public class CustomerService : Customer.CustomerBase
{
    private readonly ILogger<CustomerService> _logger;
    public CustomerService(ILogger<CustomerService> logger)
    {
        _logger = logger;
    }

    public override Task<CustomerDataModel> GetCustomerInfo(CustomerFindModel request, ServerCallContext context)
    {
        if(request.UserId == 1){
            return Task.FromResult(new CustomerDataModel
            {
                FirstName = "John",
                LastName = "Doe",
                });
        }else{
            return Task.FromResult(new CustomerDataModel
            {
                FirstName = "Jane",
                LastName = "Doe",
                });
        }
        // return base.GetCustomerInfo(request, context);
    }

    public override async Task GetAllCustomers(AllCustomerModel request, IServerStreamWriter<CustomerDataModel> responseStream, ServerCallContext context)
    {
        var  customers = new List<CustomerDataModel>
        {
            new CustomerDataModel
            {
                FirstName = "John",
                LastName = "Doe",
            },
            new CustomerDataModel
            {
                FirstName = "Jane",
                LastName = "Doe",
            },
            new CustomerDataModel
            {
                FirstName = "John",
                LastName = "Smith",
            },
            new CustomerDataModel
            {
                FirstName = "Jane",
                LastName = "Smith",
            },
            new CustomerDataModel
            {
                FirstName = "John",
                LastName = "Johnson",
            },
        };
        foreach (var customer in customers)
        {
                await responseStream.WriteAsync(customer);
                await Task.Delay(1000);
        }
    }
}
