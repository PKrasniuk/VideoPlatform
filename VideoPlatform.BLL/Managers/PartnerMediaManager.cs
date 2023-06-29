using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class PartnerMediaManager : IPartnerMediaManager
{
    private readonly IPartnerMediaRepository _partnerMediaRepository;

    public PartnerMediaManager(IPartnerMediaRepository partnerMediaRepository)
    {
        _partnerMediaRepository =
            partnerMediaRepository ?? throw new ArgumentNullException(nameof(partnerMediaRepository));
    }
}