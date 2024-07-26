namespace Providers.Application.RegistrationApplication.SignContract.TheWrongWay
{
    public sealed class SignContractService
    {
        //private readonly RegistrationApplicationRepository registrationApplicationRepository;
        //private readonly ContractsRepository contractsRepository;

        void Sign(SignContractRequest request)
        {
            //auto contract = contractsRepository.SearchById(request.ContractId);
            //auto signedContract = contract.Sign();
            //contractsRepository.Update(signedContract)
        }

        void SignEvenWorst(SignContractRequest request)
        {
            //auto application = registrationApplicationRepository.SearchById(request.ProviderRegistrationApplicationId);
            //if(application.Contract.IsAlreadySigned()) return error

            //auto contract = contractsRepository.SearchById(request.ContractId);
            //auto signedContract = contract.Sign();
            //contractsRepository.Update(signedContract)
        }
    }
}
