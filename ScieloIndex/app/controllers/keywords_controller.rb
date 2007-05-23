class KeywordsController < ScieloIndexController
  def initialize
    @model = Keyword
    @created_msg = 'The keyword was created!'
    @updated_msg = 'The keyword was updated!'
  end
end
