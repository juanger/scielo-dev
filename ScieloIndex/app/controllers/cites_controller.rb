class CitesController < ScieloIndexController
  def initialize
    @model = Cite
    @created_msg = 'The cite was created'
    @updated_msg = 'The cite was updated'
  end
end
