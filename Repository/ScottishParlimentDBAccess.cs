using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScottishParliamentMembers.Repository
{
    class ScottishParlimentDBAccess
    {
        public List<Member> GetAllMembersNames()
        {
            using (ScottishParliamentEntities db = new ScottishParliamentEntities())
            {
                List<Member> query = new List<Member>();
                try
                {
                    query = db.Members.Where(m => m.IsActive == true).ToList();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                    Console.ReadLine();
                }
                return query;
            }
        }

        public void AddMember(Member member)
        {
            using(var context = new ScottishParliamentEntities())
            {
                member.IsActive = true;
                context.Members.Add(member);
                try
                {
                    context.SaveChanges();
                }catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.ReadLine();
                }
            }
        }

        public Member GetMemberInfoById(int id)
        {
            using(var context = new ScottishParliamentEntities())
            {
                Member member = context.Members.First(x => x.PersonID == id);
                return member;
            }
        }

        public void UpdateMember(Member member)
        {
            using(var context = new ScottishParliamentEntities())
            {
                Member memberFromDB = context.Members.First(x => x.PersonID == member.PersonID);
                memberFromDB.ParliamentaryName = member.ParliamentaryName;
                memberFromDB.PreferredName = member.PreferredName;
                memberFromDB.BirthDate = member.BirthDate;
                memberFromDB.BirthDateIsProtected = member.BirthDateIsProtected;
                memberFromDB.GenderTypeID = member.GenderTypeID;
                memberFromDB.Notes = member.Notes;
                memberFromDB.PhotoURL = member.PhotoURL;
                memberFromDB.IsActive = member.IsActive;
                context.SaveChanges();
            }
        }
    }
}
