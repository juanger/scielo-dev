class ArticleKeywordsController < ScieloIndexController
  def initialize
    @model = ArticleKeyword
    @created_msg = 'The data was created!'
    @updated_msg = 'The data was updated!'
  end
end
