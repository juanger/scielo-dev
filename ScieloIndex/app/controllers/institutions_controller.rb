class InstitutionsController < ScieloIndexController
  def initialize
    @model = Institution
    @created_msg = 'The institution was created!'
    @updated_msg = 'The institution was updated!'
  end
end
