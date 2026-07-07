using Application.DTOs.Email;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Features.ContactUs
{
    public class TestMailCommand : IRequest<Response<string>>
    {
        public required string Email { get; set; }

        public class TestMailCommandHandler : IRequestHandler<TestMailCommand, Response<string>>
        {
            private readonly IEmailService _email;

            public TestMailCommandHandler(IEmailService email)
            {
                _email = email;
            }

            public async Task<Response<string>> Handle(TestMailCommand command, CancellationToken cancellationToken)
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var emailTemplate = "EmailTemplate/TestMail.cshtml";

                    await _email.SendFluentEmailTemplate(new EmailRequest()
                    {
                        Subject = $"Test Mail",
                        FirstName = "John",
                        Body = "This is just a simple plain old test mail, nothing too fancy to worry about",
                        To = command.Email,
                    }, emailTemplate);

                    ts.Complete();
                }

                return new Response<string>("message sent successfully");
            }
        }
    }
}
