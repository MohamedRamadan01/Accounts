namespace Accounts.UnitTest 
{
    public class StatementsServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public StatementsServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void GetStatements_ReturnsStatements()
        {
            // Arrange
            var service = new StatementsService(_mockUnitOfWork.Object);
            _mockUnitOfWork.Setup(uow => uow.Repository<Statement>().Include(s=> s.Account))
                .Returns(MockStatements());

            // Act
            var result = service.GetStatements();

            // Assert
            NUnit.Framework.Assert.NotNull(result.statements);
            NUnit.Framework.Assert.IsEmpty(result.ErrorMessage);
        }


        private IQueryable<Statement> MockStatements()
        {
            var statements = new List<Statement>
            {
                new Statement { ID = 1, DateField = DateTime.Now.ToString(), Amount = "100", AccountID = 1, Account = new Account() },
                new Statement { ID = 2, DateField = DateTime.Now.ToString(), Amount = "200", AccountID = 2, Account = new Account() }
            };

            return statements.AsQueryable();
        }
    }
}