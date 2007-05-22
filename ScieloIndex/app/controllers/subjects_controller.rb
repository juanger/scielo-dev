class SubjectsController < ScieloIndexController
  def initialize
    @model = Subject
    @created_msg = 'The subject was created!'
    @updated_msg = 'The subject was updated!'
  end
end
