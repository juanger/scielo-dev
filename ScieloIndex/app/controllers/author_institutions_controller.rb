class AuthorInstitutionsController < ScieloIndexController
  def initialize
    @model = AuthorInstitution
    @created_msg = 'The data was created!'
    @updated_msg = 'The data was updated!'
  end
end
