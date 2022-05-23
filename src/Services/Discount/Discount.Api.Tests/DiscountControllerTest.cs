using Discount.Api.Controllers;
using Discount.Api.Entities;
using Discount.Api.Exceptions;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Discount.Api.Tests
{
    public class DiscountControllerTest
    {
        protected DiscountController ControllerUnderTest { get; }
        protected Mock<IDiscountRepository> CouponServiceMock { get; }
        protected Mock<ILogger<DiscountController>> Logger { get; }

        public DiscountControllerTest()
        {
            CouponServiceMock = new Mock<IDiscountRepository>();
            Logger = new Mock<ILogger<DiscountController>>();
            ControllerUnderTest = new DiscountController(Logger.Object,CouponServiceMock.Object);
        }

        public class ReadAllAsync : DiscountControllerTest
        {
            [Fact]
            public async void Should_return_OkObjectResult_with_all_Coupon()
            {
                // Arrange
                var expectedCoupons = new List<Coupon>
                {
                    new Coupon { ProductName = "Test Product 1" },
                    new Coupon { ProductName = "Test Product 2" },
                    new Coupon { ProductName = "Test Product 3" }
                };
                CouponServiceMock
                    .Setup( x => x.GetAll())
                    .ReturnsAsync(expectedCoupons);

                // Act
                var result = await ControllerUnderTest.GetAll();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedCoupons, okResult.Value);
            }
        }

        public class ReadAllInClanAsync : DiscountControllerTest
        {
            [Fact]
            public async void Should_return_OkObjectResult_with_all_products()
            {
                // Arrange
                var productName = "Test Product 1";
                var expectedCoupons = new Coupon { ProductName = "Test Product 1" };

                CouponServiceMock
                    .Setup(x => x.Get(productName))
                    .ReturnsAsync(expectedCoupons);

                // Act
                var result = await ControllerUnderTest.Get(productName);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedCoupons, okResult.Value);
            }
        }

        public class ReadOneAsync : DiscountControllerTest
        {
            [Fact]
            public async void Should_return_OkObjectResult_with_a_Coupon()
            {
                // Arrange
                var productName = "Test ProductName 1";
                var expectedCoupon = new Coupon { ProductName = "Test ProductName 1" };
                CouponServiceMock
                    .Setup(x => x.Get(productName))
                    .ReturnsAsync(expectedCoupon);

                // Act
                var result = await ControllerUnderTest.Get(productName);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Same(expectedCoupon, okResult.Value);
            }

            [Fact]
            public async void Should_return_NotFoundResult_when_CouponNotFoundException_is_thrown()
            {
                // Arrange
                var productName = "Product1";
                CouponServiceMock
                    .Setup(x => x.Get(productName))
                    .ThrowsAsync(new CouponNotFoundException(productName));

                // Act
                var result = await ControllerUnderTest.Get(productName);

                // Assert
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }

        public class CreateAsync : DiscountControllerTest
        {
            [Fact]
            public async void Should_return_CreatedAtActionResult_with_the_created_Coupon()
            {
                // Arrange
                var expectedCouponProductName = "SomeCouponKey";

                var expectedCoupon = new Coupon
                {
                    ProductName = expectedCouponProductName,
                    Amount = 1000,
                    Description = "",
                };

                CouponServiceMock
                    .Setup(x => x.Create(expectedCoupon));

                // Act
                var result = await ControllerUnderTest.Create(expectedCoupon);

                // Assert
                var createdResult = Assert.IsType<OkResult>(result);
                Assert.Equal((int)HttpStatusCode.OK,createdResult.StatusCode);
            }

            [Fact]
            public async void Should_return_BadRequestResult()
            {
                // Arrange
                var Coupon = new Coupon { ProductName = "Test ProductName 1" };
                ControllerUnderTest.ModelState.AddModelError("Key", "Some error");

                // Act
                var result = await ControllerUnderTest.Create(Coupon);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.IsType<SerializableError>(badRequestResult.Value);
            }
        }

        public class UpdateAsync : DiscountControllerTest
        {
            [Fact]
            public async void Should_return_OkObjectResult_with_the_updated_Coupon()
            {
                // Arrange
                var expectedCoupon = new Coupon { ProductName = "Test Productname 1" };
                CouponServiceMock
                    .Setup(x => x.Update(expectedCoupon));

                // Act
                var result = await ControllerUnderTest.Update(expectedCoupon);

                // Assert
                var createdResult = Assert.IsType<OkResult>(result);
                Assert.Equal((int)HttpStatusCode.OK, createdResult.StatusCode);
            }

            [Fact]
            public async void Should_return_NotFoundResult_when_CouponNotFoundException_is_thrown()
            {
                // Arrange
                var unexistingCoupon = new Coupon { ProductName = "Test Productname 1" };
                CouponServiceMock
                    .Setup(x => x.Update(unexistingCoupon))
                    .ThrowsAsync(new CouponNotFoundException(unexistingCoupon.ProductName));

                // Act
                var result = await ControllerUnderTest.Update(unexistingCoupon);

                // Assert
                Assert.IsType<NotFoundObjectResult>(result);
            }

            [Fact]
            public async void Should_return_BadRequestResult()
            {
                // Arrange
                var Coupon = new Coupon { ProductName = "Test Productname 1" };
                ControllerUnderTest.ModelState.AddModelError("Key", "Some error");

                // Act
                var result = await ControllerUnderTest.Update(Coupon);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.IsType<SerializableError>(badRequestResult.Value);
            }
        }

        public class DeleteAsync : DiscountControllerTest
        {
            [Fact]
            public async void Should_return_OkObjectResult_with_the_deleted_Coupon()
            {
                // Arrange
                var CouponKey = "Some key";
                var expectedCoupon = new Coupon { ProductName = "Test Coupon 1" };
                CouponServiceMock
                    .Setup(x => x.Delete(CouponKey));

                // Act
                var result = await ControllerUnderTest.Delete(CouponKey);

                // Assert
                var createdResult = Assert.IsType<OkResult>(result);
                Assert.Equal((int)HttpStatusCode.OK, createdResult.StatusCode);
            }

            [Fact]
            public async void Should_return_NotFoundResult_when_CouponNotFoundException_is_thrown()
            {
                // Arrange
                var unexistingCouponKey = "Some Coupon key";
                CouponServiceMock
                    .Setup(x => x.Delete(unexistingCouponKey))
                    .ThrowsAsync(new CouponNotFoundException(unexistingCouponKey));

                // Act
                var result = await ControllerUnderTest.Delete(unexistingCouponKey);

                // Assert
                Assert.IsType<NotFoundObjectResult>(result);
            }
        }
    }

}
