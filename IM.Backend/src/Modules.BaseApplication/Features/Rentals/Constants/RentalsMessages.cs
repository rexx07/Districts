namespace Modules.BaseApplication.Features.Rentals.Constants;

public static class RentalsMessages
{
    public const string RentalNotExists = "Rental not exists.";

    public const string RentalCanNotBeUpdatedWhenThereIsAnotherRentedCarForTheDate =
        "Rental can't be updated when there is another rented vehicle for the date.";

    public const string RentalCanNotBeCreatedWhenCustomerFindeksCreditScoreLowerThanCarMinFindeksScore =
        "Rental can not be created when customer findeks credit score lower than vehicle min findeks score.";
}