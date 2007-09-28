class LanguagesController < ScieloIndexController
  def initialize
    @model = Language
    @created_msg = 'The language was created'
    @updated_msg = 'The language was updated'
  end
end
