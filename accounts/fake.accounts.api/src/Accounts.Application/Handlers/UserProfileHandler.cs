using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Application.Exceptions;
using Accounts.Application.Mappers.UserProfileMappers;
using Accounts.Core.DTO.Requests;
using Accounts.Core.DTO.Responses;
using Accounts.Core.Entities;
using Accounts.Core.Handlers;
using Accounts.Core.Repositories;

namespace Accounts.Application.Handlers
{
    public class UserProfileHandler : IUserProfileHandler
    {
        private const string MSG_PROFILE_NOT_FOUND = "Profile not found";
        public readonly IUserProfileRepository _userProfileRepository;
        public readonly IProfileRepository _profileRepository;

        public UserProfileHandler(
            IUserProfileRepository userProfileRepository,
            IProfileRepository profileRepository)
        {
            _userProfileRepository = userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
        }

        public async Task<UserProfileResponse> GetOrCreateAsync(UserProfileRequest userProfileRequest)
        {
            var profiles = await _profileRepository.GetAsync(w => w.AppId == userProfileRequest.AppId);
            
            var userProfile = await GetUserProfileAsync(userProfileRequest, profiles);

            if(userProfile != null)
                return userProfile.ToResponse();

            var profile = GetProfileByIdOrDefault(userProfileRequest, profiles);

            userProfileRequest.PrifileId = profile.Id;

            userProfile = await _userProfileRepository.AddAsync(userProfileRequest.ToUserProfile());

            return userProfile.ToResponse();

        }

        private async Task<UserProfile> GetUserProfileAsync(UserProfileRequest userProfileRequest, IEnumerable<Profile> profiles)
        {
            var userProfile = await _userProfileRepository.GetFirstByUserIdAndProfileId(
                userProfileRequest.UserId,
                userProfileRequest.PrifileId);

            if (userProfile != null)
                return userProfile;

            userProfile = await _userProfileRepository.GetFirstByUserIdAndProfilesIds(
                userProfileRequest.UserId,
                profiles.Select(s => s.Id));

            return userProfile;
        }

        private Profile GetProfileByIdOrDefault(UserProfileRequest userProfileRequest, IEnumerable<Profile> profiles)
        {
            var profile = profiles.FirstOrDefault(w =>
                w.AppId == userProfileRequest.AppId &&
                (
                    userProfileRequest.PrifileId == null &&
                    w.IsDefault
                ) ||
                (
                    userProfileRequest.PrifileId != null &&
                    w.Id == userProfileRequest.PrifileId
                ));

            if (profile == null)
                throw new NotFoundException(MSG_PROFILE_NOT_FOUND);
                
            return profile;
        }
    }
}