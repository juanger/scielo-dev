# Methods added to this helper will be available to all templates in the application.
module ApplicationHelper
  def languages_links
    langs = []
    [[:default, "english"], [:es_MX, "español"]].each do |lang|
      next if lang[0] == session[:lang]
      next if !session[:lang] && lang[0] == :default
      langs << link_to(lang[1], :controller => "citation_index", :action => "change_language", :lang => lang[0])
    end
    langs.join(" | ")
  end
end
