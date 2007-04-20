class ArticleAuthorsController < ScieloIndexController
  def initialize
    @model = ArticleAuthor
    @created_msg = 'The data was created!'
    @updated_msg = 'The data was updated!'
  end
end
