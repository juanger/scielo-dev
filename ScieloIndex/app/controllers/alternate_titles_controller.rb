class AlternateTitlesController < ScieloIndexController
  def initialize
    @model = AlternateTitle
    @created_msg = 'The alternate title was created.'
    @updated_msg = 'The alternate title was updated'
  end
end
