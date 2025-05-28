using ERP_System;
using System.Diagnostics.Metrics;
using Xunit;

namespace ERP_System_Test
{
    public class Address_UnitTest
    {
        //[Fact]
        //public void TestCaseAddressFail()
        //{
        //    // Arrange
        //    string Street = "Struervej";
        //    string City = "Aalborg";
        //    string State = "Nordjylland";
        //    string HouseNumber = "70";
        //    string PostalCode = "9280";
        //    string StreetNumber = "70";
        //    string Country = "Danmark";

        //    // Act
        //    Address addressTestObject = new Address();

        //    addressTestObject.Street = Street;
        //    addressTestObject.City = City;
        //    addressTestObject.State = State;
        //    addressTestObject.HouseNumber = HouseNumber;
        //    addressTestObject.PostalCode = PostalCode;
        //    addressTestObject.StreetNumber = StreetNumber;
        //    addressTestObject.Country = Country;

        //    string addressString = addressTestObject.GetAddress();
            
        //    // Assert
        //    string addressStringTrimmed = addressString.Trim();
        //    string[] stringsInAddressString = addressStringTrimmed.Split(new char[] { ',' });

        //    Assert.Equal(HouseNumber, stringsInAddressString[0]);
        //    Assert.Equal(Street, stringsInAddressString[1]);
        //    Assert.Equal(StreetNumber, stringsInAddressString[2]);
        //    Assert.Equal(City, stringsInAddressString[3]);
        //    Assert.Equal(State, stringsInAddressString[4]);
        //    Assert.Equal(PostalCode, stringsInAddressString[5]);
        //    Assert.Equal(Country, stringsInAddressString[6]);
        //    //return $"{HouseNumber},{Street}, {StreetNumber}, {City}, {State}, {PostalCode}, {Country}";
        //}

        [Fact]
        public void TestCaseAddressPass()
        {
            // Arrange
            string Street = "Struervej";
            string City = "Aalborg";
            string State = "Nordjylland";
            string HouseNumber = "70";
            string PostalCode = "9280";
            string StreetNumber = "70";
            string Country = "Danmark";

            // Act
            Address addressTestObject = new Address();

            addressTestObject.Street = Street;
            addressTestObject.City = City;
            addressTestObject.State = State;
            addressTestObject.HouseNumber = HouseNumber;
            addressTestObject.PostalCode = PostalCode;
            addressTestObject.StreetNumber = StreetNumber;
            addressTestObject.Country = Country;

            string addressString = addressTestObject.GetAddress();

            // Assert
            string addressStringTrimmed = addressString.Trim();
            string[] stringsInAddressString = addressStringTrimmed.Split(new char[] { ',' });
            //string[] stringsInAddressString = addressStringTrimmed.Split(',');

            Assert.Equal(HouseNumber, stringsInAddressString[0]);
            Assert.Equal(Street, stringsInAddressString[1]);
            Assert.Equal(" " + StreetNumber, stringsInAddressString[2]);
            Assert.Equal(" " + City, stringsInAddressString[3]);
            Assert.Equal(" " + State, stringsInAddressString[4]);
            Assert.Equal(" " + PostalCode, stringsInAddressString[5]);
            Assert.Equal(" " + Country, stringsInAddressString[6]);
            //return $"{HouseNumber},{Street}, {StreetNumber}, {City}, {State}, {PostalCode}, {Country}";
        }
    }
}
