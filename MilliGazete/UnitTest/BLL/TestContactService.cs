using Business.Constants;
using Business.Managers.Abstract;
using Business.Managers.Concrete;
using Core.Utilities.Helper.Abstract;
using Entity.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.DAL;
using UnitTest.Extra;
using Xunit;
namespace UnitTest.BLL
{
    public class TestContactService : TestContactDal
    {

        #region setup
        private readonly IContactService _contactService;
        private readonly IMailHelper _mailHelper;
        public TestContactService()
        {
            var _mapper = new TestAutoMapper()._mapper;
            _contactService = new ContactManager(new ContactAssistantManager(contactDal,_mailHelper, _mapper), _mapper);
        }
        #endregion

        #region methods

        [Theory(DisplayName = "GetById")]
        [Trait("Contact", "Get")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task ServiceShouldReturnContactById(int id)
        {
            // arrange
            // act
            var result = await _contactService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(result.Data.Email, db.Contacts.FirstOrDefault(f => f.Id == id).Email);
        }
        [Theory(DisplayName = "GetByIdError")]
        [Trait("Contact", "Get")]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public async Task ServiceShouldReturnErrorForNotValidContactId(int id)
        {
            // arrange
            // act
            var result = await _contactService.GetById(id);
            // assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal(result.Message, Messages.RecordNotFound);
        }

        [Fact(DisplayName = "Add")]
        [Trait("Contact", "Add")]
        public async Task ServiceShouldAddNewContactToList()
        {
            // arrange
            var contact = new ContactAddDto()
            {
                Email = "ShakibaITADD@gmail.com",
                Phone = "05524353057",
                FullName = "Saeid Shakiba",
                Message = "Message for add"
            };
            // act
            var result = await _contactService.Add(contact);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            var newContact = db.Contacts.FirstOrDefault(f => f.Email == contact.Email);
            Assert.NotNull(newContact);
            Assert.Equal(newContact.CreatedAt.Date, DateTime.Now.Date);
            Assert.Equal(result.Message, Messages.Added);

        }
        #endregion
    }
}
