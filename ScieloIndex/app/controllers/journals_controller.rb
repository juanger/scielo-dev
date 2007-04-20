class JournalsController < ScieloIndexController
  def initialize
    @model = Journal
    @created_msg = 'The journal was created!'
    @updated_msg = 'The journal was created!'
  end
end
