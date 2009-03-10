class CitationsController < ScieloIndexController
  def initialize
    @model = Citations
    @created_msg = 'The citation was created'
    @updated_msg = 'The citation was updated'
  end
end
