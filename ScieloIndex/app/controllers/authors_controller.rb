class AuthorsController < ScieloIndexController
  def initialize
    @model = Author
    @created_msg = 'The author was created!'
    @updated_msg = 'The author was updated!'
  end
end
