namespace Accounts.UnitTest
{
    public class StatementsControllerTests
    {
        private readonly Mock<IStatementService> _mockStatementService;
        private readonly Mock<ILogger<StatementsController>> _mockLogger;

        public StatementsControllerTests()
        {
            _mockStatementService = new Mock<IStatementService>();
            _mockLogger = new Mock<ILogger<StatementsController>>();
        }

        [Fact]
        public void GetStatements_AdminRole_ReturnsOk()
        {
            // Arrange
            var controller = new StatementsController(_mockStatementService.Object, _mockLogger.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = MockClaimsPrincipal("Admin")
                }
            };

            _mockStatementService.Setup(s => s.GetStatements(It.IsAny<SearchParameters>()))
                .Returns(new GetStatementsResult { IsSuccess = true, statements = new List<StatementDto>() });

            // Act
            var result = controller.GetStatements(new SearchParameters());

            // Assert
            Xunit.Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public void GetStatements_InvalidUserRole_ReturnsUnauthorized()
        {
            // Arrange
            var controller = new StatementsController(_mockStatementService.Object, _mockLogger.Object);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = MockClaimsPrincipal("Admin")
                }
            };

            // Act
            var result = controller.GetStatements(new SearchParameters());

            // Assert
            Xunit.Assert.IsType<UnauthorizedResult>(result);
        }


        private ClaimsPrincipal MockClaimsPrincipal(string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "TestUser"),
                new Claim("Role", role)
            };

            var identity = new ClaimsIdentity(claims, "Test");
            return new ClaimsPrincipal(identity);
        }
    }

   
}