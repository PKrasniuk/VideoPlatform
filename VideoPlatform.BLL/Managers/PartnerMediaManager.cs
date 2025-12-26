using System;
using VideoPlatform.BLL.Interfaces;
using VideoPlatform.DAL.Interfaces;

namespace VideoPlatform.BLL.Managers;

public class PartnerMediaManager(IPartnerMediaRepository partnerMediaRepository) : IPartnerMediaManager
{
    private readonly IPartnerMediaRepository _partnerMediaRepository =
        partnerMediaRepository ?? throw new ArgumentNullException(nameof(partnerMediaRepository));
}