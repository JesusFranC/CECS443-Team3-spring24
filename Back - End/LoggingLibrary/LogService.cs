﻿
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Team3.ThePollProject.Controllers;
using Team3.ThePollProject.Services;
using Team3.ThePollProject.Models;
using Team3.ThePollProject.Models.Response;



namespace Team3.ThePollProject.LoggingLibrary
{


    public class LogService : ILogService
    {
        private readonly ILogTarget _logTarget;
        private readonly IHashService _hashService;

        public LogService(ILogTarget logTarget, IHashService hashService)
        {
            _logTarget = logTarget;
            _hashService = hashService;
        }
        private string createLogHash(string logDetails)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(logDetails));

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public IResponse CreateLog(string logLevel, string logCategory, string logContext, string? userHash = null)
        {
            DateTime time = DateTime.UtcNow;
            string logDetails = time.ToString() + logLevel + logCategory + logContext + userHash;
            string logHash = createLogHash(logDetails);
            //changed to work with log object
            ILog log = new Log(time, logLevel, logCategory, logContext, logHash, userHash);
            return _logTarget.WriteLog(log);
        }

        public async Task<IResponse> CreateLogAsync(string logLevel, string logCategory, string logContext, string? userHash = null)
        {
            DateTime time = DateTime.UtcNow;
            string logDetails = time.ToString() + logLevel + logCategory + logContext + userHash;
            string logHash = createLogHash(logDetails);
            ILog log = new Log(time, logLevel, logCategory, logContext, logHash, userHash);
            return await Task.Run(() => _logTarget.WriteLog(log));
        }
    }
}
