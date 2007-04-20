class PublishersController < ScieloIndexController
  def initialize
    @model = Publisher
    @created_msg = 'The publisher was created!'
    @updated_msg = 'The publisher was created!'
  end
end
