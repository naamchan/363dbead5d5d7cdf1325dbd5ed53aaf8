#nullable enable

using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared
{
    public static class FirebaseManager
    {
        private static FirebaseUser? loggedInUser = default;

        private static int cachedHighscore = 0;
        public static int CachedHighscore => cachedHighscore;

        private const string HIGHSCORE_KEY = "highscore";

        public static async Task AnonymousLogin()
        {
            if (FirebaseAuth.DefaultInstance.CurrentUser == null)
            {
                var loginResult = await FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync();
                loggedInUser = loginResult.User;
            }
            else
            {
                loggedInUser = FirebaseAuth.DefaultInstance.CurrentUser;
            }
        }

        public static async Task FetchPlayerHighscore()
        {
            if(loggedInUser == null)
            {
                throw new InvalidOperationException("Plase login before fetch highscore");
            }

            var documentRef = GetDocumentRef();
            var snapshot = await documentRef.GetSnapshotAsync();
            Dictionary<string, object> document = snapshot.ToDictionary();

            if (document != null && document.ContainsKey(HIGHSCORE_KEY))
            {
                cachedHighscore = Convert.ToInt32(document[HIGHSCORE_KEY]);
            }
        }

        public static async Task UpdatePlayerHighscore(int newHighscore)
        {
            if (loggedInUser == null)
            {
                throw new InvalidOperationException("Plase login before update highscore");
            }

            cachedHighscore = newHighscore;

            var documentRef = GetDocumentRef();
            Dictionary<string, object> document = new Dictionary<string, object>()
            {
                { HIGHSCORE_KEY, newHighscore }
            };
            await documentRef.SetAsync(document);
        }

        private static DocumentReference GetDocumentRef() => FirebaseFirestore.DefaultInstance.Collection("users").Document(loggedInUser!.UserId);
    }
}