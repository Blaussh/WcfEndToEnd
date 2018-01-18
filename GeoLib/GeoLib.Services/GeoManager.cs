using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoLib.Contracts;
using GeoLib.Data;

namespace GeoLib.Services
{
    public class GeoManager : IGeoService
    {
        private IZipCodeRepository _zipCodeRepository = null;
        private IStateRepository _stateRepository = null;

        public GeoManager(IStateRepository stateRepository, IZipCodeRepository zipCodeRepository)
        {
            _stateRepository = stateRepository;
            _zipCodeRepository = zipCodeRepository;
        }

        public GeoManager(IZipCodeRepository zipCodeRepository)
        {
            _zipCodeRepository = zipCodeRepository;
        }

        public GeoManager(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        public ZipCodeData GetZipInfo(string zip)
        {
            ZipCodeData zipCodeData = null;
            IZipCodeRepository zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();
            ZipCode zipCodeEntity = zipCodeRepository.GetByZip(zip);
            if (zipCodeEntity != null)
            {
                zipCodeData = new ZipCodeData()
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State.Abbreviation,
                    ZipCode = zipCodeEntity.Zip
                };
            }
            return zipCodeData;
        }

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            List<string> stateData = new List<string>();

            IStateRepository stateRepository = _stateRepository ?? new StateRepository();

            IEnumerable<State> states = stateRepository.Get(primaryOnly);

            if (states != null)
            {
                foreach (var state in states)
                {
                    stateData.Add(state.Abbreviation);
                }
            }

            return stateData;
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            List<ZipCodeData> zips = new List<ZipCodeData>();

            IZipCodeRepository zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();

            var zipCodes = zipCodeRepository.GetByState(state);

            if (zipCodes != null)
            {
                foreach (var zipCode in zipCodes)
                {
                    zips.Add(new ZipCodeData()
                    {
                        City = zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode = zipCode.Zip
                    });
                }
            }

            return zips;
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            List<ZipCodeData> zips = new List<ZipCodeData>();

            IZipCodeRepository zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();

            var zipCodes = zipCodeRepository.GetZipsForRange(zipCodeRepository.GetByZip(zip), range);

            if (zipCodes != null)
            {
                foreach (var zipCode in zipCodes)
                {
                    zips.Add(new ZipCodeData()
                    {
                        City = zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode = zipCode.Zip
                    });
                }
            }

            return zips;
        }
    }
}
