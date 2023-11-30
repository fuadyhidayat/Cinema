using Logic.Services.CurrentUser;
using Logic.Services.Email;
using Logic.Services.Storage;

namespace Logic.Documents.RemoveDocument;

public class RemoveDocumentLogic(
    DataAccessService dataAccessService,
    StorageService storageService,
    EmailService emailService,
    ICurrentUserService currentUserService)
    : IRequestHandler<RemoveDocumentInput, RemoveDocumentOutput>
{
    public async Task<RemoveDocumentOutput> Handle(RemoveDocumentInput input, CancellationToken cancellationToken = default)
    {
        var document = await dataAccessService.Documents
            .Where(x => x.Id == input.Id)
            .SingleAsync(cancellationToken);

        await storageService.DeleteAsync(document.FileCode);

        _ = dataAccessService.Documents.Remove(document);
        _ = await dataAccessService.SaveChangesAsync(cancellationToken);

        var sendEmailInput = new SendEmailInput
        {
            ToName = currentUserService.Name,
            ToEmailAddress = currentUserService.Email,
            Subject = "Anda telah menghapus dokumen.",
            Body = $"Anda telah menghapus dokumen {document.Title}."
        };

        await emailService.SendEmail(sendEmailInput, cancellationToken);

        return new RemoveDocumentOutput();
    }
}
