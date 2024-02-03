using Anthropic;
using Anthropic.SDK;
using Anthropic.SDK.Completions;
using Anthropic.SDK.Constants;


string? continueKey = "y";
while(continueKey == "y" || continueKey == "Y" || continueKey == ""){
	Console.WriteLine("Enter your API key: ");
	var userApiKey = Console.ReadLine();
	var apiKey = userApiKey;
	var client = new AnthropicClient(apiKey);

	// Anthropic API key checker
	if (!(apiKey == null) && apiKey.StartsWith("sk-ant"))
	{
		try
		{
			var prompt =
				$@"{AnthropicSignals.HumanSignal} Say yes or no. {AnthropicSignals.AssistantSignal}";

			var parameters = new SamplingParameters()
			{
				MaxTokensToSample = 10,
				Prompt = prompt,
				Temperature = 0.0m,
				StopSequences = new[] { AnthropicSignals.HumanSignal },
				Stream = false,
				Model = AnthropicModels.ClaudeInstant_v1
			};
			var response = await client.Completions.GetClaudeCompletionAsync(parameters);
			Console.WriteLine(response.Completion);
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}
	}

	// Openai API key checker
	else if (!(apiKey == null) && apiKey.StartsWith("sk-"))
	{
		try
		{
			var api = new OpenAI_API.OpenAIAPI($"{apiKey}");
			var result = await api.Chat.CreateChatCompletionAsync("Hello!");
			Console.WriteLine(result);
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}
	}
	else
	{
		Console.WriteLine("Invalid API key");
	}
}
