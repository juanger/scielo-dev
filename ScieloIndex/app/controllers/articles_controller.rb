class ArticlesController < ScieloIndexController
  def initialize
    @model = Article
    @created_msg = 'The article was created!'
    @updated_msg = 'The article was updated!'
  end
end
