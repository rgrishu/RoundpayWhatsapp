
using System.Collections.Generic;

namespace EmailService
{
	public interface IEmailService
	{
		void Send(EmailMessage emailMessage, EmailConfiguration _emailConfig);
		List<EmailMessage> ReceiveEmail(int maxCount = 10);
	}
}
