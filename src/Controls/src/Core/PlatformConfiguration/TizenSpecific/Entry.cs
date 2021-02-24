namespace Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific
{
	using FormsElement = Maui.Controls.Entry;

	public static class Entry
	{
		public static readonly BindableProperty FontWeightProperty = BindableProperty.Create("FontWeight", typeof(string), typeof(FormsElement), FontWeight.None);

		public static string GetFontWeight(BindableObject element)
		{
			return (string)element.GetValue(FontWeightProperty);
		}

		public static void SetFontWeight(BindableObject element, string weight)
		{
			element.SetValue(FontWeightProperty, weight);
		}

		public static string GetFontWeight(this IPlatformElementConfiguration<Tizen, FormsElement> config)
		{
			return GetFontWeight(config.Element);
		}

		public static IPlatformElementConfiguration<Tizen, FormsElement> SetFontWeight(this IPlatformElementConfiguration<Tizen, FormsElement> config, string weight)
		{
			SetFontWeight(config.Element, weight);
			return config;
		}
	}
}
