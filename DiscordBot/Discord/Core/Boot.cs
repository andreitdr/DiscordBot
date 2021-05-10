using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using static DiscordBotPluginManager.Functions;

namespace DiscordBot.Discord.Core
{
	public class Boot
	{
		private readonly string              botPrefix;
		private readonly string              botToken;
		private readonly Label               loggedInLabel;
		private readonly RichTextBox         rtb;
		public           DiscordSocketClient client;
		private          CommandHandler      commandServiceHandler;
		private          CommandService      service;


		public Boot(string botToken, string botPrefix, RichTextBox rtb, Label loggedInLabel)
		{
			this.botToken      = botToken;
			this.rtb           = rtb;
			this.loggedInLabel = loggedInLabel;
			this.botPrefix     = botPrefix;
		}

		public async Task Awake()
		{
			client  = new DiscordSocketClient();
			service = new CommandService();

			CommonTasks();


			await client.LoginAsync(TokenType.Bot, botToken);
			await client.StartAsync();

			commandServiceHandler = new CommandHandler(client, service, botPrefix[0]);
			await commandServiceHandler.InstallCommandsAsync();

			await Task.Delay(-1);
		}

		private void CommonTasks()
		{
			client.LoggedOut += Client_LoggedOut;
			client.Log       += Log;
			client.LoggedIn  += LoggedIn;
			client.Ready     += Ready;
		}

		private Task Client_LoggedOut()
		{
			WriteLogFile("Successfully Logged Out");
			Log(new LogMessage(LogSeverity.Info, "Boot", "Successfully logged out from discord !"));
			return Task.CompletedTask;
		}

		private Task Ready()
		{
			loggedInLabel.Invoke(new MethodInvoker(delegate
			{
				loggedInLabel.Text      = "ONLINE";
				loggedInLabel.ForeColor = Color.Green;
			}));
			return Task.CompletedTask;
		}

		private Task LoggedIn()
		{
			loggedInLabel.Invoke(new MethodInvoker(delegate
			{
				loggedInLabel.Text      = "CONNECTED";
				loggedInLabel.ForeColor = Color.Gold;
			}));
			WriteLogFile("The bot has been logged in at " + DateTime.Now.ToShortDateString() + " (" +
						 DateTime.Now.ToShortTimeString() + ")");
			return Task.CompletedTask;
		}

		private Task Log(LogMessage message)
		{
			rtb.Invoke(new MethodInvoker(delegate
			{
				switch (message.Severity)
				{
					case LogSeverity.Error:
					case LogSeverity.Critical:
						WriteErrFile(message.Message);
						rtb.AppendText("[ERROR] " + message.Message + "\n");
						break;

					case LogSeverity.Info:
					case LogSeverity.Debug:
						WriteLogFile(message.Message);
						rtb.AppendText("[INFO] " + message.Message + "\n");
						break;
				}
			}));
			return Task.CompletedTask;
		}
	}
}
