using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace TaskFlapKap.Application.Midddleware
{
	public class ExeptionHandling
	{
		private readonly RequestDelegate next;

		public ExeptionHandling(RequestDelegate next)
		{
			this.next = next;
		}
		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				var response = context.Response;
				response.ContentType = "application/json";
				var obj = new { Message = ex.Message, IsSuccessfuly = false, StatusCode = GetStatusCode(ex) };
				var responsemodel = JsonSerializer.Serialize(obj);
				response.StatusCode = GetStatusCode(ex);
				await response.WriteAsync(responsemodel);
			}
		}
		private int GetStatusCode(Exception ex)
		{
			return ex switch
			{
				InvalidDataException => (int)HttpStatusCode.BadRequest,
				ValidationException => (int)HttpStatusCode.BadRequest,
				_ => (int)HttpStatusCode.InternalServerError,
			};
		}
	}
}
