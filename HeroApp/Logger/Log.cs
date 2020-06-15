using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace HeroApp.Logger
{
    public class Log
    {
        private DataBaseFirstEntities _context;

        public Log(DataBaseFirstEntities context)
        {
            _context = context is null ? new DataBaseFirstEntities() : context;
        }

        public void LogException(ExceptionContext filterContext)
        {
            LogException(filterContext?.Exception);
        }

        /// <summary>
        /// Logs errors into the DataBase
        /// </summary>
        /// <param name="filterContext">Exception information to be logged</param>
        public void LogException(Exception exception)
        {
            var msg = exception?.Message;
            var inner = exception?.InnerException?.Message;
            var stack = exception?.StackTrace;

            // Create the Exception Log object to be logged with desired lengths.
            _context.ExceptionLog.Add(new ExceptionLog()
            {
                Exception = msg.Length > 1000 ? msg.Substring(0, 999) : msg,
                InnerException = inner is null ? string.Empty : (inner.Length > 1000 ? inner.Substring(0, 999) : inner),
                StackTrace = stack.Length > 5000 ? stack.Substring(0, 4999) : stack
            });

            try
            {
                //Log the error!!
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                // Do whatever desired with exception
                throw;
            }
        }
    }
}