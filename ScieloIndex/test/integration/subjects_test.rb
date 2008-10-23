require "#{File.dirname(__FILE__)}/../test_helper"

class SubjectsTest < ActionController::IntegrationTest
  fixtures :subjects

  def setup
    @subjects = [:geo, :atm, :bioqui, :fisica, :quimica]
    @mysubject = {:parent_id => 1, :name => 'Mecanica Cuantica'}
  end

   def test_getting_index
     get "/subjects"
     assert_equal 200, status
     assert_equal '/subjects', path
   end

   def test_showing
     @subjects.each { | subject |
       post "/subjects/show", :id => subjects(subject).id
       assert 200, status
       assert_equal '/subjects/show', path
     }
   end

   def test_editing
     @subjects.each { | subject |
       post "/subjects/edit", :id => subjects(subject).id
       assert 200, status
       assert "/subjects/edit/#{subjects(subject).id}", path
     }
   end

   def test_updating_name
     @subjects.each { | subject |
       post "/subjects/update", :id => subjects(subject).id, :parent => subjects(subject).name.reverse
       assert 302, status
       follow_redirect!
       assert "/subjects/show/#{subjects(subject).id}", path
     }
   end

    def test_deleting
     @subjects.each { | subject |
       get "/subjects/destroy", :id => subjects(subject).id
       assert 302, status
       assert '/subjects/list',  path
     }
   end
end
