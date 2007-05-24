class ArticleSubjectsController < ScieloIndexController
  def initialize
    @model = ArticleSubject
    @created_msg = 'The data was created!'
    @updated_msg = 'The data was updated!'
  end
end
